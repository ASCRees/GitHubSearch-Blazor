using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Services.Models;


namespace GitHubSearch_Blazor.Profiles
{
    public class GitHubUserViewModelProfile:Profile
    {
        public GitHubUserViewModelProfile()
        {
            CreateMap<GitHubUserServiceModel, GitHubUserViewModel>();
        }
    }
}
