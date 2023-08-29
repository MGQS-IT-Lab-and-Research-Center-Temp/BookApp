using BookApp.DTOs.Flag;
using BookApp.Models;
using BookApp.Models.Flag;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookApp.Service.Interface;

public interface IFlagService
{
    Task<BaseResponseModel> CreateFlag(FlagCreateDto createFlagDto);
    Task<BaseResponseModel> DeleteFlag(string flagId);
    Task<BaseResponseModel> UpdateFlag(string flagId, FlagUpdateDto FlagDto);
    Task<FlagResponseModel> GetFlag(string flagId);
    Task<FlagsResponseModel> GetAllFlag();
    Task<IEnumerable<SelectListItem>> SelectFlags();
}
