﻿namespace Vimata.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.ViewModels.Lessons;

    public interface ILessonService
    {
        Task<IEnumerable<string>> GetLessons();
        Task<Lesson> GetLessonByName(string lesson);
    }
}
