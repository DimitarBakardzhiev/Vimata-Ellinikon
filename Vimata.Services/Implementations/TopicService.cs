namespace Vimata.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.Data.Repositories;
    using Vimata.Services.Contracts;
    using Vimata.ViewModels.ViewModels.Topics;

    public class TopicService : ITopicService
    {
        private readonly IRepository<Topic> topicsRepository;

        public TopicService(IRepository<Topic> topicsRepository)
        {
            this.topicsRepository = topicsRepository;
        }

        public async Task CreateTopic(CreateTopicVM topic, string rootPath)
        {
            string path = Path.Combine(rootPath, "Images", "Topics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string extention = topic.Image.FileName.Substring(topic.Image.FileName.LastIndexOf("."));
            path = Path.Combine(path, Guid.NewGuid().ToString() + extention);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await topic.Image.CopyToAsync(fileStream);
            }

            await topicsRepository.AddAsync(new Topic() { Title = topic.Title, Image = path });
        }

        public async Task<IEnumerable<TopicIndexVM>> GetAll()
        {
            var topics = (await topicsRepository.GetAllAsync()).Select(t => new TopicIndexVM() { 
                Title = t.Title,
                Image = Convert.ToBase64String(File.ReadAllBytes(t.Image))
            }).ToList();

            return topics;
        }
    }
}
