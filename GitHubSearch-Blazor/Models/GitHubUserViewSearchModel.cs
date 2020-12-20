using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.Models
{
    [ExcludeFromCodeCoverage]
    public class GitHubUserViewSearchModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(40, ErrorMessage = "Search too long (40 char).")]
        public string UserNameSearch { get; set; }

        public List<GitHubUserViewModel> UserViewModel { get; set; }
    }
}