using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrummage.Core.Services.Validation;

namespace Scrummage.Core.Services
{
    public interface IService
    {
        void Initialize(IValidationDictionary validationDictionary);
    }
}
