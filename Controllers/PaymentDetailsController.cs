using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        private readonly ILogger<PaymentDetailsController> _logger;

        public PaymentDetailsController(PaymentDetailContext context, ILogger<PaymentDetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
        {
            try
            {
                if (_context.PaymentDetails == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("working fine.");
                return await _context.PaymentDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentsDetails(int id)
        {
            try
            {
                if (_context.PaymentDetails == null)
                {
                    return BadRequest();
                }
                var a = await _context.PaymentDetails.FindAsync(id);
                if (a == null)
                {
                    return NotFound();
                }

                return Ok(a);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetails(int id, PaymentDetails paymentDetails)
        {
            try
            {
                if (id != paymentDetails.PaymentDetailId)
                {
                    return BadRequest();
                }

                _context.Entry(paymentDetails).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDetailsExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(await _context.PaymentDetails.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PutPaymentsDetails(int id, PaymentDetails paymentDetails)
        {
            try
            {
                if (_context.PaymentDetails == null)
                {
                    return Problem("Null");
                }
                _context.PaymentDetails.Add(paymentDetails);
                await _context.SaveChangesAsync();

                return Ok(await _context.PaymentDetails.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetails(int id)
        {
            try
            {
                if (_context.PaymentDetails == null)
                {
                    return NotFound();
                }

                var a = await _context.PaymentDetails.FindAsync(id);
                if (a != null)
                {
                    _context.PaymentDetails.Remove(a);
                    await _context.SaveChangesAsync();
                }

                return Ok(await _context.PaymentDetails.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        private bool PaymentDetailsExist(int id)
        {
            try
            {
                return (_context.PaymentDetails?.Any(e => e.PaymentDetailId == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
