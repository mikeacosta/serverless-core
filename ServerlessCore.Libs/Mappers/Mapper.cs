using System.Collections.Generic;
using System.Linq;
using ServerlessCore.Data.Models;
using ServerlessCore.Libs.Models;

namespace ServerlessCore.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<ContentItem> ToContentItem(IEnumerable<ContentData> items)
        {
            return items.Select(ToContentItem);
        }

        public ContentItem ToContentItem(ContentData item)
        {
            return new ContentItem
            {
                Name = item.Name,
                Value = item.Value
            };
        }

        public ContentData ToContentData(ContentItem item)
        {
            return new ContentData
            {
                Name = item.Name,
                Value = item.Value
            };
        }

        public IEnumerable<Job> ToJob(IEnumerable<JobData> jobs)
        {
            return jobs.Select(ToJob);
        }

        public Job ToJob(JobData job)
        {
            return new Job()
            {
                Id = job.Id,
                Company = job.Company,
                Title = job.Title,
                Start = job.Start,
                End = job.End,
                Website = job.Website,
                Image = job.Image,
                Content = job.Content.Select(c => c.ToString()).ToList()
            };
        }

        public JobData ToJobData(Job job)
        {
            return new JobData()
            {
                Id = job.Id,
                Company = job.Company,
                Title = job.Title,
                Start = job.Start,
                End = job.End,
                Website = job.Website,
                Image = job.Image,
                Content = job.Content.Select(c => c.ToString()).ToList()
            };
        }

        public IEnumerable<ProfileItem> ToProfileItem(IEnumerable<ProfileData> items)
        {
            return items.Select(ToProfileItem);
        }

        public ProfileItem ToProfileItem(ProfileData item)
        {
            return new ProfileItem
            {
                Id = item.Id,
                Type = item.Type,
                Text = item.Text
            };
        }

        public ProfileData ToProfileData(ProfileItem item)
        {
            return new ProfileData
            {
                Id = item.Id,
                Type = item.Type,
                Text = item.Text
            };
        }
    }
}
