using ServerlessCore.Data.Models;
using ServerlessCore.Libs.Models;
using System.Collections.Generic;

namespace ServerlessCore.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<Job> ToJob(IEnumerable<JobData> jobs);

        Job ToJob(JobData job);

        JobData ToJobData(Job job);

        IEnumerable<ProfileItem> ToProfileItem(IEnumerable<ProfileData> items);

        ProfileItem ToProfileItem(ProfileData item);

        ProfileData ToProfileData(ProfileItem item);

        IEnumerable<ContentItem> ToContentItem(IEnumerable<ContentData> items);

        ContentItem ToContentItem(ContentData item);

        ContentData ToContentData(ContentItem item);
    }
}
