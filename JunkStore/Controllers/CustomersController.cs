using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JunkStore.Data;
using JunkStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace JunkStore.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private UserManager<ApplicationUser> userManager;

        public CustomersController(ApplicationDbContext context, UserManager<ApplicationUser> usrMgr, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _context = context;
            this.passwordHasher = passwordHasher;
            this.userManager = usrMgr;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Customer.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,Password,PhoneNumber,Address")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = customer.Email,
                    Email = customer.Email
                };
                IdentityResult result = await userManager.CreateAsync(appUser, customer.Password);
                if (result.Succeeded)
                {
                    customer.CustomerId = Guid.NewGuid();
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(customer);
                }


            }
            return View(customer);
        }



        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CustomerId,FirstName,LastName,Email,Password,PhoneNumber,Address")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id.ToString());
                    if (user != null)
                    {
                        user.Email = customer.Email;
                        user.PhoneNumber = customer.PhoneNumber;

                        if (!string.IsNullOrEmpty(customer.Password))
                        {
                            user.PasswordHash = passwordHasher.HashPassword(user, customer.Password);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Password cannot be empty");
                        }

                        var result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            _context.Update(customer);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Errors(result);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Not Found");
                    }   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(customer);
        }


        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Customer'  is null.");
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(Guid id)
        {
          return _context.Customer.Any(e => e.CustomerId == id);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
