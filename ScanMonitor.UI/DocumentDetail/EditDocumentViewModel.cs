using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ScanMonitor.Database.GetCorrespondents;
using ScanMonitor.Database.GetDocumentForEdit;
using ScanMonitor.Database.GetPeople;

namespace ScanMonitor.UI.DocumentDetail
{
    public class EditDocumentViewModel
    {
        public EditDocumentViewModel()
        {
            PeopleRefdata = GetPeopleQuery.List();
            CorrespondentsRefdata = GetCorrespondentsQuery.List();
        }

        public List<PersonDto> PeopleRefdata { get; }
        public List<CorrespondentDto> CorrespondentsRefdata { get; }

        public string DocumentType { get; set; }

        public string PersonId { get; set; }
        public string CorrespondentId { get; set; }
        public DateTime Datum { get; set; }
        public string Beschrijving { get; set; }

        public ObservableCollection<CustomFieldDto> CustomFields { get; set; }
        public ObservableCollection<ScanDto> Scans { get; set; }
    }
}