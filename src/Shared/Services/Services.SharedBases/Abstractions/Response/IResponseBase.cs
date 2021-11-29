using OLM.Services.SharedBases.APIErrors;

namespace OLM.Services.SharedBases.Abstractions.Response
{
    internal interface IResponseBase : IBase
    {
        bool Success { get; set; }

        APIError Errors { get; set; }
    }
}