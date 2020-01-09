namespace Vimata.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.ViewModels.ViewModels.Topics;

    public interface ITopicService
    {
        Task CreateTopic(CreateTopicVM topic, string rootPath);
        Task<IEnumerable<TopicIndexVM>> GetAll();
    }
}
