using OLM.Services.CategoryBulbs.API.Services.Validator.Abstractions;
using OLM.Services.CategoryBulbs.API.Services.Validator.Abstractions.Collection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace OLM.Services.CategoryBulbs.API.Services.Validator.Implementations.Collection
{
    // TODO: Egy ValidateAll Metodus hozzáadása, ami az összes validálja és egy custom modellel tér vissza, ami tartalmazza a validatort és hogy sikeresen vagy hibásan futtot le
    public class CategoryValidationCollection : ICategoryValidatorCollection
    {
        public ICategoryValidator this[int index] { get => _validators[index]; set => _validators[index] = value; }

        private List<ICategoryValidator> _validators;

        public CategoryValidationCollection()
        {
            _validators = new List<ICategoryValidator>();
        }

        public int Count => _validators.Count;

        public bool IsReadOnly => false;

        public void Add(ICategoryValidator item) => _validators.Add(item);

        public void Clear() => _validators.Clear();

        public bool Contains(ICategoryValidator item) => _validators.Contains(item);

        public void CopyTo(ICategoryValidator[] array, int arrayIndex) => _validators.CopyTo(array, arrayIndex);

        public IEnumerator<ICategoryValidator> GetEnumerator() => _validators.GetEnumerator();

        public int IndexOf(ICategoryValidator item) => _validators.IndexOf(item);

        public void Insert(int index, ICategoryValidator item) => _validators.Insert(index, item);

        public bool Remove(ICategoryValidator item) => _validators.Remove(item);

        public void RemoveAt(int index) => _validators.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => _validators.GetEnumerator();

        public IEnumerable<ValidationResult> ValidateAll()
        {
            throw new NotImplementedException();
        }
    }
}
