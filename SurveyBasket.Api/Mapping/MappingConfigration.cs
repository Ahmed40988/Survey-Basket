using SurveyBasket.Api.Contracts.Users;
using SurveyBasket.Api.ContractsDTO.Questions;

namespace SurveyBasket.Api.Mapping
{
    public class MappingConfigration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // if you find any property is has differant name from DTO ,Model
            //config.NewConfig<Poll, PollResponse>()
            //    .Map(des => des.Notes, src => src.Description);

            //config.NewConfig<Student, StudentResponse>()
            //    .Map(des => des.FullName, src => $"{src.Fname} {src.Mname} {src.Lname} ")
            //    .Map(dest => dest.age, src => DateTime.Now.Year - src.BirthDay!.Value.Year,
            //    srccon => srccon.BirthDay.HasValue)
            //    .Map(des => des.departname, src => src.Department.Name)
            //    .Ignore(des => des.Title);
            //config.NewConfig<Poll, PollResponse>().TwoWays();

            config.NewConfig<RegisterRequest, ApplicationUser>()
                .Map(dis => dis.UserName, src => src.Email);

            config.NewConfig<QuestionRequest, Question>()
                .Map(dest => dest.Answers, src => src.Answers.Select(answer => new Answer { Content = answer }));


            config.NewConfig<(ApplicationUser user, IList<string> roles), UserResponse>()
                .Map(dest => dest, src => src.user)
                .Map(dest => dest.Roles, src => src.roles);

            config.NewConfig<CreateUserRequest, ApplicationUser>()
                .Map(dest => dest.UserName, src => src.Email)
                .Map(dest => dest.EmailConfirmed, src => true);

            //config.NewConfig<UpdateUserRequest, ApplicationUser>()
            //    .Map(dest => dest.UserName, src => src.Email)
            //    .Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper());





        }
    }
}
