using Mission.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(Missions mission);
        Task<IList<Missions>> ClientSideMissionList();

        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);

        List<MissionApplication> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
    }
}
