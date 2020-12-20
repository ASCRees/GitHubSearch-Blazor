using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
//using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Exceptions;
using GitHubMemberSearch.Services.Models;
using GitHubSearch_Blazor.Pages;
using GitHubSearch_Blazor.Profiles;
using Microsoft.Extensions.Configuration;

namespace GitHubMemberSearch.ModelBuilders
{
    public class HomeModelBuilder : IHomeModelBuilder
    {
        public Search SearchObj { get; set; }
        private IMapper _mapper;

        IConfiguration _configuration;
        public HomeModelBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<GitHubUserViewModelProfile>();
            });
            _mapper = config.CreateMapper();
        }
 

        //public HomeController HomeControllerObj { get; set; }
        public async Task<GitHubUserViewModel> BuildSearchViewModel(string userNameSearch)
        {
            GitHubUserViewModel gitHubUserViewModel = new GitHubUserViewModel();

            string defaultUrl = _configuration["RootUrl"] +
                                _configuration["UsersUrl"];

            //HomeController.Log.Info($"Request for details of \"{userNameSearch}\"");
            if (!string.IsNullOrEmpty(userNameSearch))
            {
                try
                {
                        GitHubUserServiceModel gitHubUserServiceModel =
                            await SearchObj.CallGitHubService.CallUserApi(string.Format(defaultUrl, userNameSearch));
                    if (gitHubUserServiceModel != null)
                    {
                        gitHubUserViewModel = _mapper.Map<GitHubUserViewModel>(gitHubUserServiceModel);

                        if (gitHubUserViewModel.id > 0)
                        {
                            List<GitHubUserReposServiceModelItem> gitHubUserReposServiceModelItem =
                                await SearchObj.CallGitHubService.CallUserReposApi(gitHubUserViewModel.repos_url);
                            if (gitHubUserReposServiceModelItem != null)
                            {
                                if (gitHubUserReposServiceModelItem.Count > 0)
                                {
                                    //gitHubUserViewModel.reposItems =
                                    //    Mapper.Map<List<GitHubUserReposServiceModelItem>, List<GitHubUserReposViewModelItem>>(
                                    //        gitHubUserReposServiceModelItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        string noRecordsFound = $"No records found for user \"{userNameSearch}\"";
                      //  HomeController.Log.Warn(noRecordsFound);
                        gitHubUserViewModel.message = noRecordsFound;
                    }
                }
                catch (HttpResponseException ex)
                {
                    string noRecordsFound = $"No records found for user \"{userNameSearch}\"";
                    //HomeController.Log.Warn(noRecordsFound);
                    gitHubUserViewModel.message = noRecordsFound;
                }
                catch
                {
                    throw;
                }
            }

            return gitHubUserViewModel;
        }

        internal static GitHubUserViewSearchModel GetUserViewSearchModel(string userNameSearch)
        {
            GitHubUserViewSearchModel gitHubUserViewSearchModel = new GitHubUserViewSearchModel();
            gitHubUserViewSearchModel.UserNameSearch = string.Empty;
            
            if (!string.IsNullOrWhiteSpace(userNameSearch))
            {
                gitHubUserViewSearchModel.UserNameSearch = userNameSearch;
            }
            
            return gitHubUserViewSearchModel;
        }
    }
}