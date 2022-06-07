using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyClient.Models
{
  public class LoginM
  {
    [Required(ErrorMessage = "Please Enter Your Firstname")]
    public string Firstname { get; set; }
    [Required(ErrorMessage = "Please Enter Your Lastname")]
    public string Lastname { get; set; }
  }
}
