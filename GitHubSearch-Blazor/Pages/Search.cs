using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitHubMemberSearch.ModelBuilders;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Service.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;

namespace GitHubSearch_Blazor.Pages
{
    public partial class Search
    {
        [Inject] ICallGitHubService GitHubService { get; set; }
        [Inject] IHomeModelBuilder HomeBuilder { get; set; }

        [Inject] IConfiguration Configuration { get; set; }

        private GitHubUserViewSearchModel searchModel = new GitHubUserViewSearchModel();

        //internal static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(HomeController));
        internal ICallGitHubService CallGitHubService;
       // private readonly IHomeModelBuilder _homeModelBuilder;

        private bool formInvalid = true;
        private EditContext editContext;
        protected override void OnInitialized()
        {
          //  var CallGitHubService = GitHubService;
            HomeBuilder.SearchObj = this;
            HomeBuilder.SearchObj.CallGitHubService = GitHubService;
            editContext = new EditContext(searchModel);
            editContext.OnFieldChanged += HandleFieldChanged;
            var rootUrl = Configuration["RootUrl"];

        }

        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            formInvalid = !editContext.Validate();
            StateHasChanged();
        }

        private void HandleValidSubmit()
        {

            var userNameSearch = searchModel.UserNameSearch;
            var end = "Stop";


            GitHubUserViewSearchModel gitHubUserViewSearchModel = HomeModelBuilder.GetUserViewSearchModel(userNameSearch);
            try
            {
                var gitHubUserViewModel = HomeBuilder.BuildSearchViewModel(userNameSearch).GetAwaiter().GetResult();
                if (!string.IsNullOrWhiteSpace(userNameSearch))
                {
                    searchModel.UserViewModel = new List<GitHubUserViewModel>
                    {
                        gitHubUserViewModel
                    };
                    //gitHubUserViewSearchModel.UserViewModel = new List<GitHubUserViewModel>
                    //{
                    //    gitHubUserViewModel
                    //};
                }

            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                //return Redirect("~/ErrorHandler");
            }
        }

        public void Dispose()
        {
            editContext.OnFieldChanged -= HandleFieldChanged;
        }
    }
}
