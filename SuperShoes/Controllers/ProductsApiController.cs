using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SuperShoes.Model;
using System.Linq.Expressions;

namespace SuperShoes.Controllers
{
    [Route("services/products")]
    [ApiController]
    public class ProductsApiController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsApiController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<Response>> GetProducts()
        {
            Response response = new Response();
            try
            {
                List<Product> productList = await _context.Products.Where(p => p.StatusId < StatusIdCancelled).ToListAsync<Product>();

                response.Success = true;
                response.Component = productList;
            }
            catch (Exception ex)
            {
                response.ErrorCode = ResultServerError;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetProduct(int id)
        {
            Response response = new Response();
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "ProductId not found in database";
                return NotFound(response);
            }
            response.Success = true;
            response.Component = product;
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutProduct(int id, Product product)
        {
            Response response = new Response();

            if (id != product.ProductId)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "ProductId error";

                return BadRequest();
            }

            Product dbProduct = await _context.Products.Where(item => item.Code == product.Code && item.ProductId != product.ProductId && item.StatusId < StatusIdCancelled).FirstOrDefaultAsync();
            if (dbProduct != null)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "Product code already exists";
                return BadRequest(response);
            }

            _context.Entry<Product>(product).State = EntityState.Modified;

            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultDBError;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultBadRequest;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
            }
            return await GetProduct(product.ProductId);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> PostProduct(Product product)
        {
            Response response = new Response();
            Product dbProduct = await _context.Products.Where(item => item.Code == product.Code && item.StatusId < StatusIdCancelled).FirstOrDefaultAsync();
            if (dbProduct != null)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "Product code already exists";
                return BadRequest(response);
            }
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    product.StatusId = StatusIdNew;
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultDBError;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultBadRequest;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
            }
            return await GetProduct(product.ProductId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteProduct(int id)
        {
            Response response = new Response();
            Product product = await _context.Products.FindAsync((object)id);
            if (product == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "ProductId not found";
                return NotFound(response);
            }
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    product.StatusId = StatusIdCancelled;
                    await _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultDBError;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultBadRequest;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
            }
            response.Success = true;

            return response;
        }

        [HttpGet("{id}/existences")]
        public ActionResult<Response> GetInventory(int id)
        {
            Response response = new Response();

            IQueryable<ProductExistenceDTO> productExistences = _context.Set<ProductExistenceDTO>().FromSqlRaw("exec dbo.sp_ProductExistence @ProductId={0}", id);

            if (productExistences == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "Inventory for ProductId not found in database";
                return NotFound(response);
            }

            response.Success = true;
            response.Component = productExistences;

            return response;
        }
    }
}
