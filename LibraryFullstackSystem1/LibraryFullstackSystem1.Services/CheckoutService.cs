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
            var now = DateTime.Now;

            var item = _DbContext.LibraryAssets.FirstOrDefault(p => p.Id == libraryCardId);

            //Remove any existing checkouts in the item
            RemoveExistingCheckouts(assetId);

            //Close any existing history
            CloseExistingCheckoutHistory(assetId, now);

            //Look for existing hold
            var currentHold = _DbContext.Holds
                         .Include(p => p.LibraryAsset)
                         .Include(p => p.LibraryCard)
                         .Where(p => p.LibraryAsset.Id == assetId);

            //if Holds, checkout item to the earlist hold
            if (currentHold.Any())
            {
                CheckoutToEarliestHold(assetId, currentHold);
            }
            else
            {
                // else mark item status to available
                MarkItem(assetId, "Available");
            }

            _DbContext.SaveChanges();

        }

        public void CheckoutToEarliestHold(int assetId, IQueryable<Hold> currentHold)
        {
            var earlistHold = currentHold
                .Include(p => p.LibraryCard)
                .Include(p => p.LibraryAsset)
                .OrderBy(p => p.HoldPlaced).FirstOrDefault(p => p.LibraryAsset.Id == assetId);

            var libraryCard = earlistHold.LibraryCard;

            _DbContext.Remove(earlistHold);
            _DbContext.SaveChanges();

            CheckOutItem(assetId, libraryCard.Id);

        }

        public void CheckOutItem(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;

            if (IsCheckedOut(assetId))
            {
                return;
            }
            else
            {
                var item = _DbContext.LibraryAssets.FirstOrDefault(p => p.Id == assetId);

                MarkItem(assetId, "Checked Out");

                var card = _DbContext.LibraryCards
                    .Include(p => p.Checkouts)
                    .FirstOrDefault(p => p.Id == libraryCardId);

                var newCheckOut = new Checkout
                {
                    LibraryAsset = item,
                    LibraryCard = card,
                    Since = now,
                    Until = GetDefaultCheckoutTime(now)
                };

                _DbContext.Add(newCheckOut);

                var checkoutHistory = new CheckoutHistory
                {
                    CheckedOut = now,
                    LibraryAsset = item,
                    LibraryCard = card
                };

                _DbContext.Add(checkoutHistory);
                _DbContext.SaveChanges();
            }
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }
    

        public bool IsCheckedOut(int assetId)
        {
            return _DbContext.Checkouts.Where(p => p.LibraryAsset.Id == assetId).Any();
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
            var item = _DbContext.Holds
                .Include(p => p.LibraryAsset)
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.Id == id);

            var cardId = item?.LibraryCard.Id;

            var patronName = _DbContext.Patrons
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryCard.Id == cardId);

            return patronName?.FirstName + " " + patronName?.LastName;
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            return _DbContext.Holds
                .Include(p => p.LibraryAsset)
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.Id == id)
                .HoldPlaced;

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
            var now = DateTime.Now;

            var item = _DbContext.LibraryAssets.FirstOrDefault(p => p.Id == assetId);

            var card = _DbContext.LibraryCards.FirstOrDefault(p => p.Id == libraryCardId);

            if (item.Status.Name == "Available")
            {
                MarkItem(assetId, "On Hold");

                var newHold = new Hold
                {  
                    LibraryAsset = item,
                    LibraryCard = card,
                    Id = assetId,
                    HoldPlaced = now
                };

                _DbContext.Add(newHold);
                _DbContext.SaveChanges();
            }
     
        }

        public string GetCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);

            if (checkout == null)
            {
                return "";
            }
            else
            {
                var cardId = checkout.LibraryCard.Id;
                var patron = _DbContext.Patrons.Include(p => p.LibraryCard)
                    .FirstOrDefault(p => p.LibraryCard.Id == cardId);

                return patron.FirstName + " " + patron.LastName;
            }

        }

        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _DbContext.Checkouts
                .Include(p => p.LibraryAsset)
                .Include(p => p.LibraryCard)
                .FirstOrDefault(p => p.LibraryAsset.Id == assetId);
        }
    }
}
