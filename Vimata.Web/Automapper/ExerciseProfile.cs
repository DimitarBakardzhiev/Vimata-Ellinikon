namespace Vimata.Web.Automapper
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Vimata.Data.Models;
    using Vimata.ViewModels.ViewModels;
    using Vimata.ViewModels.ViewModels.Exercises;
    using static Vimata.ViewModels.ViewModels.ExerciseSearchResultVM;

    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<ClosedExercise, ClosedExerciseVM>().ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => o.Content)));
            CreateMap<OpenExercise, OpenExerciseVM>();
            CreateMap<DragAndDropExercise, DragAndDropExerciseVM>().ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => o.Content)));
            CreateMap<SpeakingExercise, SpeakingExerciseVM>();

            CreateMap<ClosedExercise, ExerciseSearchResultVM>().ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.Closed))
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id));
            CreateMap<OpenExercise, ExerciseSearchResultVM>().ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.Open))
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id));
            CreateMap<DragAndDropExercise, ExerciseSearchResultVM>().ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.DragAndDrop))
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id));
            CreateMap<SpeakingExercise, ExerciseSearchResultVM>().ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.Speaking))
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
