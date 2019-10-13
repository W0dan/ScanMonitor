using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanMonitor.Database._Interfaces
{
    public interface ICustomFieldDto
    {
        string Id { get; set; }

        string DocumentTypeCustomFieldId { get; set; }

        string FieldName { get; set; }
        string FieldType { get; set; }

        string StringValue { get; set; }
        decimal? NumericValue { get; set; }
        bool? BooleanValue { get; set; }
        DateTime? DateValue { get; set; }
    }
}
