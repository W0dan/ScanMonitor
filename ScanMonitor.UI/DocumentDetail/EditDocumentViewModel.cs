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
        private List<PersonDto> peopleRefdata;
        private List<CorrespondentDto> correspondentsRefdata;

        public IEnumerable<PersonDto> PeopleRefdata => peopleRefdata ?? (peopleRefdata = GetPeopleQuery.List());
        public IEnumerable<CorrespondentDto> CorrespondentsRefdata => correspondentsRefdata ?? (correspondentsRefdata = GetCorrespondentsQuery.List());

        public string DocumentType { get; set; }

        public string PersonId { get; set; }
        public string CorrespondentId { get; set; }
        public DateTime Datum { get; set; }
        public string Beschrijving { get; set; }

        public ObservableCollection<CustomFieldDto> CustomFields { get; set; }
        public ObservableCollection<ScanDto> Scans { get; set; }
        public string Id { get; set; }
    }
}