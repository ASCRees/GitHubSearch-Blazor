using GitHubMemberSearch.Models;
using GitHubSearch_Blazor.Pages;
using System.Threading.Tasks;

namespace GitHubMemberSearch.ModelBuilders
{
    public interface IHomeModelBuilder
    {
        Search SearchObj { get; set; }

        //HomeController HomeControllerObj { get; set; }
        Task<GitHubUserViewModel> BuildSearchViewModel(string userNameSearch);
    }
}