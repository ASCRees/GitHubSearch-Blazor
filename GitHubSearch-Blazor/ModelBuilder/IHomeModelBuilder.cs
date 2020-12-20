using GitHubMemberSearch.Models;
using System.Threading.Tasks;

namespace GitHubMemberSearch.ModelBuilders
{
    public interface IHomeModelBuilder
    {
        //HomeController HomeControllerObj { get; set; }
        Task<GitHubUserViewModel> BuildSearchViewModel(string userNameSearch);
    }
}