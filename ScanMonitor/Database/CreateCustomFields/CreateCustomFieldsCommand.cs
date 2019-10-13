using ScanMonitor.Database.GetCustomFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMonitor.Database.CreateCustomFields
{
    public class CreateCustomFieldsCommand
    {
        public List<CustomFieldDto> CustomFields { get; internal set; }
        public string DocumentId { get; internal set; }
    }
}
