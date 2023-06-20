using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Tech_task.Data;
using Tech_task.Models;

namespace Tech_task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : Controller
    {
        private readonly WarehouseAPIDbContext dbContext;
        public WarehouseController(WarehouseAPIDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{tableName}")]
        public async Task<IActionResult> GetRecordsByType(string tableName)
        {
            switch (tableName.ToLower())
            {
                case "worker":
                    return Ok(await dbContext.Worker.ToListAsync());

                case "product":
                    return Ok(await dbContext.Product.ToListAsync());

                case "department":
                    return Ok(await dbContext.Department.ToListAsync());
                default:
                    return BadRequest("Invalid Name");
            }
        }

        [HttpGet]
        [Route("{tableName}/{id:guid}")]
        public async Task<IActionResult> GetRecord([FromRoute] string tableName, [FromRoute] Guid id)
        {
            switch (tableName.ToLower())
            {
                case "worker":
                    return await GetRecordById<Worker>(id);

                case "product":
                    return await GetRecordById<Product>(id);

                case "department":
                    return await GetRecordById<Department>(id);

                default:
                    return BadRequest("Invalid Name");
            }
        }

        private async Task<IActionResult> GetRecordById<T>(Guid id) where T : class
        {
            var record = await dbContext.Set<T>().FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        //[HttpPost]
        //[Route("{type}")]
        //public async Task<IActionResult> AddRecord<T>([FromRoute] string type, [FromBody] T addRequest) where T : class
        //{
        //    switch (type.ToLower())
        //    {
        //        case "worker":
        //            if (addRequest is AddWorkerRequest workerRequest)
        //                return await AddWorker(workerRequest);
        //            break;

        //        case "product":
        //            if (addRequest is AddProductRequest productRequest)
        //                return await AddProduct(productRequest);
        //            break;

        //        case "department":
        //            if (addRequest is AddDepartmentRequest departmentRequest)
        //                return await AddDepartment(departmentRequest);
        //            break;
        //    }
        //    return BadRequest("Invalid type");
        //}
        [HttpPost]
        [Route("Add Worker")]
        public async Task<IActionResult> AddWorker([FromBody] AddWorkerRequest addWorkerRequest)
        {
            var worker = new Worker();

            if (worker != null)
            {
                worker.FirstName = addWorkerRequest.FirstName;
                worker.LastName = addWorkerRequest.LastName;
                worker.Work = addWorkerRequest.Work;
                worker.Phone = addWorkerRequest.Phone;
                worker.DepartmentId = addWorkerRequest.DepartmentId;

                await dbContext.Worker.AddAsync(worker);
                await dbContext.SaveChangesAsync();

                return Ok(worker);
            }

            return NotFound();
        }
        [HttpPost]
        [Route("Add Product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest addProductRequest)
        {
            var product = new Product();

            if (product != null)
            {
                product.Name = addProductRequest.Name;
                product.Manufacturer = addProductRequest.Manufacturer;
                product.YearOfProduction = addProductRequest.YearOfProduction;
                product.DepartmentId = addProductRequest.DepartmentId;

                await dbContext.Product.AddAsync(product);
                await dbContext.SaveChangesAsync();

                return Ok(product);
            }

            return NotFound();
        }
        [HttpPost]
        [Route("Add Department")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentRequest addDepartmentRequest)
        {
            var department = new Department()
            {
                Id = Guid.NewGuid(),
                Name = addDepartmentRequest.Name,
                ProductType = addDepartmentRequest.ProductType,
                Area = addDepartmentRequest.Area
            };

            await dbContext.Department.AddAsync(department);
            await dbContext.SaveChangesAsync();

            return Ok(department);
        }


        //[HttpPut]
        //[Route("{type}/{id:guid}")]
        //public async Task<IActionResult> UpdateRecords<T>([FromRoute] string type, [FromRoute] Guid id, [FromBody] T updateRequest) where T : class
        //{
        //    switch (type.ToLower())
        //    {
        //case "worker":
        //    if (updateRequest is UpdateWorkerRequest workerRequest)
        //        return await UpdateWorker(id, workerRequest);
        //    break;

        //case "product":
        //    if (updateRequest is UpdateProductRequest productRequest)
        //        return await UpdateProduct(id, productRequest);
        //    break;

        //case "department":
        //    if (updateRequest is UpdateDepartmentRequest departmentRequest)
        //        return await UpdateDepartment(id, departmentRequest);
        //    break;
        //    }

        //    return BadRequest("Invalid type");
        //}

        [HttpPut]
        [Route("Update Worker/{id:guid}")]
        public async Task<IActionResult> UpdateWorker(Guid id, UpdateWorkerRequest updateWorkerRequest)
        {
            var worker = dbContext.Worker.Find(id);

            if (worker != null)
            {
                worker.FirstName = updateWorkerRequest.FirstName;
                worker.LastName = updateWorkerRequest.LastName;
                worker.Work = updateWorkerRequest.Work;
                worker.Phone = updateWorkerRequest.Phone;
                worker.DepartmentId = updateWorkerRequest.DepartmentId;

                await dbContext.SaveChangesAsync();

                return Ok(worker);
            }

            return NotFound();
        }
        [HttpPut]
        [Route("Update Product/{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest updateProductRequest)
        {
            var product = await dbContext.Product.FindAsync(id);

            if (product != null)
            {
                product.Name = updateProductRequest.Name;
                product.Manufacturer = updateProductRequest.Manufacturer;
                product.YearOfProduction = updateProductRequest.YearOfProduction;
                product.DepartmentId = updateProductRequest.DepartmentId;

                await dbContext.SaveChangesAsync();

                return Ok(product);
            }

            return NotFound();
        }
        [HttpPut]
        [Route("Update Department/{id:guid}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, UpdateDepartmentRequest updateDepartmentRequest)
        {
            var department = await dbContext.Department.FindAsync(id);

            if (department != null)
            {
                department.Name = updateDepartmentRequest.Name;
                department.ProductType = updateDepartmentRequest.ProductType;
                department.Area = updateDepartmentRequest.Area;

                await dbContext.SaveChangesAsync();

                return Ok(department);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{tableName}/{id:guid}")]
        public async Task<IActionResult> DeleteRecord([FromRoute]string tableName, [FromRoute] Guid id)
        {
            switch (tableName.ToLower())
            {
                case "worker":
                    return await DeleteRecordById<Worker>(id);

                case "product":
                    return await DeleteRecordById<Product>(id);

                case "department":
                    return await DeleteRecordById<Department>(id);

                default:
                    return BadRequest("Invalid Name");
            }
        }
        public async Task<IActionResult> DeleteRecordById<T>(Guid id) where T : class
        {
            var record = await dbContext.Department.FindAsync(id);

            if (record != null)
            {
                dbContext.Remove(record);
                await dbContext.SaveChangesAsync();
                return Ok(record);
            }
            return NotFound();
        }
    }
}
