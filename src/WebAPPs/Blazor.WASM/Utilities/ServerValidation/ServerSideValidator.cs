using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OLM.Services.SharedBases.APIErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLM.Blazor.WASM.Utilities.ServerValidation
{
    public class ServerSideValidator : ComponentBase
    {
        private ValidationMessageStore _messageStore;

        [CascadingParameter] 
        public EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(ServerSideValidator)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(ServerSideValidator)} " +
                    $"inside an {nameof(EditForm)}.");
            }

            _messageStore = new ValidationMessageStore(CurrentEditContext);
            CurrentEditContext.OnValidationRequested += (s, e) => _messageStore.Clear();
            CurrentEditContext.OnFieldChanged += (s, e) => _messageStore.Clear(e.FieldIdentifier);
        }

        public void DisplayErrors(APIError error)
        {
            foreach (var err in error.Errors)
            {
                _messageStore.Add(CurrentEditContext.Field(err.FieldName), err.ErrorMSG);
            }
            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}
