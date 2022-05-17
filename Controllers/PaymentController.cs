﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffProjectAPI.Data;
using StaffProjectAPI.Responses;
using StaffProjectAPI.Services;
using System.Security.Claims;

namespace StaffProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public PaymentController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IPaymentService paymentService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _paymentService = paymentService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<List<PaymentModel>> GetAllPayments(string? sort_by = null, string? sort_type = null, string? s = null)
        {
            var user = await _userManager.GetUserAsync(User);

            var payments = _paymentService.GetAllPayments(user, sort_by, sort_type, s);

            return payments;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<PaymentResponse>> GetPaymentById(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            PaymentModel payment = _paymentService.GetPaymentById(user, id)?.Value;

            if (payment is null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PaymentResponse>> Create(PaymentModel payment)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = _paymentService
                .AddPayment(user, payment.Amount, payment.Currency, payment.PayerEmail, payment.DueToDate, payment.TransactionId);

            if (result.Succeeded)
                return Created("", result.Payment);
            else
                return BadRequest(result.Errors);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<PaymentResponse>> Update(int id, PaymentModel payment)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = _paymentService
                .UpdatePayment(user, id, payment.Amount, payment.Currency, payment.PayerEmail, payment.DueToDate, payment.TransactionId);

            if (result.Payment is null)
                return NotFound();
            else if (result.Succeeded)
                return Ok(result.Payment);
            else
                return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<PaymentResponse>> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = _paymentService.DeletePayment(user, id);

            if (result.Succeeded == false)
                return NotFound();
            else
                return Ok(result.Payment);
        }
    }
}
