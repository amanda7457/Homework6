using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Barron_Amanda_HW6.DAL;
using Barron_Amanda_HW6.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Barron_Amanda_HW6.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetailController(AppDbContext context)
        {
            _context = context;
        }


        // GET: OrderDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderDetail orderDetail)
        {
            //Find the related registration detail in the database
            OrderDetail DbOrdDet = _context.OrderDetails
                                        .Include(r => r.Product)
                                        .Include(r => r.Order)
                                        .FirstOrDefault(r => r.OrderDetailID ==
                                                            orderDetail.OrderDetailID);

            //update the related fields
            DbOrdDet.OrderQuantity = orderDetail.OrderQuantity;
            DbOrdDet.OrderPrice = DbOrdDet.Product.ProductPrice;
            DbOrdDet.ExtendedPrice = DbOrdDet.OrderPrice * DbOrdDet.OrderQuantity;

            //update the database
            if ( ModelState.IsValid)
            {
                _context.OrderDetails.Update(DbOrdDet);
                _context.SaveChanges();
            }
          
            //return to the order details
            return RedirectToAction("Details", "Order", new { id = DbOrdDet.Order.OrderID });
        }

        /*public async Task<IActionResult> Edit(int id, [Bind("OrderDetailID,OrderQuantity,OrderPrice,ExtendedPrice")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }*/

        // GET: OrderDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderDetailID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            Order ord = _context.Orders.FirstOrDefault(r => r.OrderDetails.Any(rd => rd.OrderDetailID == id));
            return RedirectToAction("Details", "Orders", new { id = id });
            
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailID == id);
        }
    }
}
