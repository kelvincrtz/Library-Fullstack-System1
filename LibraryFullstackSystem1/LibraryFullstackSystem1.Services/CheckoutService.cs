using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFullstackSystem1.Services
{
    public class CheckoutService : ICheckout
    {
        private readonly LibradyFullstackSystemDbContext _DbContext;
        
        public CheckoutService(LibradyFullstackSystemDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public void Add(Checkout newCheckout)
        {
            _DbContext.Add(newCheckout);
            _DbContext.SaveChanges();
        }

        public void CheckInItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _DbContext.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return _DbContext.Checkouts.FirstOrDefault(p => p.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistories(int id)
        {
            return _DbContext.CheckoutHistories
                .Where(p => p.LibraryAsset.Id == id)
                .Include(p => p.LibraryAsset)
                .Include(p => p.LibraryCard);
        }

        public string GetCurrentHoldPatronName(int id)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _DbContext.Holds
                .Include(p => p.LibraryAsset)
                .Where(p => p.Id == id);
        }

        public Checkout GetLatestCheckout(int id)
        {
            return _DbContext.Checkouts
                .Where(p => p.LibraryAsset.Id == id)
                .OrderByDescending(p => p.Since)
                .FirstOrDefault(p=>p.Id==id);
        }

        public void MarkFound(int assetId)
        {
            var now = DateTime.Now;

            //MarkItem
            MarkItem(assetId, "Found");

            //Remove any exisiting Checkouts
            RemoveExistingCheckouts(assetId);

            //Close any existing Checkout History
            CloseExistingCheckoutHistory(assetId, now);

            _DbContext.SaveChanges();
      
        }

        private void MarkItem(int assetId, string status)
        {
            var item = _DbContext.LibraryAssets.FirstOrDefault(p => p.Id == assetId);

            _DbContext.Update(item);
            item.Status = _DbContext.Statuses.FirstOrDefault(p => p.Name == status);
        }

        private void CloseExistingCheckoutHistory(int assetId, DateTime now)
        {
            var checkoutHistory = _DbContext.CheckoutHistories.FirstOrDefault(p => p.Id == assetId && p.CheckedIn == null);

            if (checkoutHistory != null)
            {
                _DbContext.Update(checkoutHistory);
                checkoutHistory.CheckedIn = now;
            }
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            var checkout = _DbContext.Checkouts.FirstOrDefault(p => p.Id == assetId);

            if (checkout != null)
            {
                _DbContext.Remove(checkout);
            }
        }

        public void MarkLost(int assetId)
        {
            MarkItem(assetId,"Lost");

            _DbContext.SaveChanges();
        }

        public void PlaceHold(int assetId, int libraryCardId)
        {
            throw new NotImplementedException();
        }
    }
}
