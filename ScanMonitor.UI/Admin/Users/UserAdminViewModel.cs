﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using ScanMonitor.Database.GetPeople;
using ScanMonitor.Database.SearchDocuments;
using ScanMonitor.Database.UserAanpassen;
using ScanMonitor.Database.UserToevoegen;
using ScanMonitor.Database.UserVerwijderen;

namespace ScanMonitor.UI.Admin.Users
{
    public class UserAdminViewModel : GenericAdminViewModel
    {
        public UserAdminViewModel()//:base(GetPeopleQuery.List().Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList())
        {
            var people = GetPeopleQuery.List()
                .Select(x => new AdminItem { Id = x.Id, Name = x.Name }).ToList();

            Items = new ObservableCollection<AdminItem>(people);
            //OriginalItems = people;
        }

        public override string Title => "Beheer van gebruikers";

        protected override bool CanBeDeleted(AdminItem item)
        {
            return !SearchDocumentsQuery.List(new SearchDocumentsRequest { PersonId = item.Id }).Any();
        }

        protected override string CannotDeleteMessage => "Gebruiker kan niet verwijderd worden, want er zijn nog documenten aanwezig in de databank die behoren to deze gebruiker.";

        protected override void AddItem(AdminItem item)
        {
            UserToevoegenQuery.Insert(new UserToevoegenCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = item.Name
            });
        }

        protected override void UpdateItem(AdminItem item)
        {
            UserAanpassenQuery.Update(new UserAanpassenCommand
            {
                Id = item.Id,
                Name = item.Name
            });
        }

        protected override void DeleteItem(AdminItem item)
        {
            UserVerwijderenQuery.Delete(new UserVerwijderenCommand
            {
                Id = item.Id
            });
        }
    }
}