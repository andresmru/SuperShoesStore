using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SuperShoes.Model;

namespace SuperShoes.Controllers
{
    [Route("services/existences")]
    [ApiController]
    public class ProductExistencesApiController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductExistencesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductExistencesApi
        [HttpGet]
        public async Task<ActionResult<Response>> GetProductExistences()
        {
            Response response = new Response();
            try
            {
                List<ProductExistence> StoreList = await _context.ProductExistences.ToListAsync<ProductExistence>();

                response.Success = true;
                response.Component = StoreList;
            }
            catch (Exception ex)
            {
                response.ErrorCode = ResultServerError;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }

            return response;
        }

        // GET: api/ProductExistencesApi/5
        [HttpGet("{storeid}/{productid}")]
        public ActionResult<Response> GetProductExistence(int storeId, int productId)
        {
            Response response = new Response();

            IEnumerable<ProductExistenceDTO> productExistences = _context.Set<ProductExistenceDTO>().FromSqlRaw("exec dbo.sp_ProductExistence @StoreId={0}, @ProductId={1}", storeId, productId);

            if (productExistences == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "Inventory for StoreId and ProductId not found in database";
                return NotFound(response);
            }

            response.Success = true;
            response.Component = productExistences.FirstOrDefault();

            return response;
        }

        // PUT: api/ProductExistencesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{storeid}/{productid}")]
        public async Task<ActionResult<Response>> PutProductExistence(int storeId, int productId, ProductExistence productExistence)
        {
            Response response = new Response();

            if (storeId != productExistence.StoreId && productId != productExistence.ProductId)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "Id error";

                return BadRequest(response);
            }

            _context.Entry(productExistence).State = EntityState.Modified;

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

            return GetProductExistence(productExistence.StoreId, productExistence.ProductId);
        }

        // POST: api/ProductExistencesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Response>> PostProductExistence(ProductExistence productExistence)
        {
            Response response = new Response();

            ProductExistence dbProductExistence = await _context.ProductExistences.Where(item => item.StoreId == productExistence.StoreId && item.ProductId == productExistence.ProductId).FirstOrDefaultAsync();
            if (dbProductExistence != null)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "Existence for store and product already exists";
                return BadRequest(response);
            }
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ProductExistences.Add(productExistence);
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
            return GetProductExistence(productExistence.StoreId, productExistence.ProductId);
        }

        // DELETE: api/ProductExistencesApi/5
        [HttpDelete("{storeid}/{productid}")]
        public async Task<ActionResult<Response>> DeleteProductExistence(int storeId, int productId)
        {
            Response response = new Response();

            var productExistence = await _context.ProductExistences.FindAsync(storeId, productId);
            if (productExistence == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "StoreId not found";
                return NotFound();
            }

            _context.ProductExistences.Remove(productExistence);
            await _context.SaveChangesAsync();

            response.Success = true;

            return response;
        }
    }
}
