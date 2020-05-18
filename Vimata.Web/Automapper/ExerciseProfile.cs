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

    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ClosedExerciseVM>().ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => o.Content)));
            CreateMap<Exercise, OpenExerciseVM>();
            CreateMap<Exercise, DragAndDropExerciseVM>().ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => o.Content)));
            CreateMap<Exercise, SpeakingExerciseVM>();

            CreateMap<Exercise, ExerciseSearchResultVM>().ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateClosedExerciseVM, Exercise>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => new ExerciseOption() { Content = o })))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.Closed))
                .ReverseMap();
            CreateMap<CreateOpenExerciseVM, Exercise>()
                .ForMember(dest => dest.AlternativeAnswers, opt => opt.MapFrom(src => src.AlternativeAnswers.Select(a => new AlternativeAnswer() { Content = a })))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.Open))
                .ReverseMap();
            CreateMap<CreateDragAndDropExerciseVM, Exercise>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(o => new ExerciseOption() { Content = o })))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.DragAndDrop))
                .ReverseMap();
            CreateMap<CreateSpeakingExerciseVM, Exercise>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ExerciseType.Speaking))
                .ReverseMap();

            CreateMap<ExercisesSession, ExercisesSessionVM>()
                .ForMember(dest => dest.ClosedExercises, opt => opt.MapFrom(src => src.Exercises.Where(e => e.Type == ExerciseType.Closed)))
                .ForMember(dest => dest.ClosedExercises, opt => opt.MapFrom(src => src.Exercises.Where(e => e.Type == ExerciseType.Open)))
                .ForMember(dest => dest.ClosedExercises, opt => opt.MapFrom(src => src.Exercises.Where(e => e.Type == ExerciseType.DragAndDrop)))
                .ForMember(dest => dest.ClosedExercises, opt => opt.MapFrom(src => src.Exercises.Where(e => e.Type == ExerciseType.Speaking)));
        }
    }
}
