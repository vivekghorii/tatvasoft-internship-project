using Mission.Entities;
using Mission.Entities.Models.CommonModels;
using Mission.Repositories.IRepositories;
using Mission.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mission.Service.Services
{
    public class CommonService(ICommonRepository commonRepository) : ICommonService
    {
        private readonly ICommonRepository _commonRepository = commonRepository;

        public List<DropDownResponseModel> CountryList()
        {
            return _commonRepository.CountryList();
        }

        public List<DropDownResponseModel> CityList(int countryId)
        {
            return _commonRepository.CityList(countryId);
        }

        public List<DropDownResponseModel> MissionCountryList()
        {
            return _commonRepository.MissionCountryList();
        }

        public List<DropDownResponseModel> MissionCityList()
        {
            return _commonRepository.MissionCityList();
        }

        public List<DropDownResponseModel> MissionThemeList()
        {
            return _commonRepository.MissionThemeList();
        }

        public List<DropDownResponseModel> MissionSkillList()
        {
            return _commonRepository.MissionSkillList();
        }

        public List<DropDownResponseModel> MissionTitleList()
        {
            return _commonRepository.MissionTitleList();
        }

        public List<DropDownResponseModel> GetUserSkill(int userId)
        {
            return _commonRepository.GetUserSkill(userId);
        }

        public async Task<bool> AddUserSkill(UserSkills skills)
        {
            return await _commonRepository.AddUserSkill(skills);
        }
    }
}
