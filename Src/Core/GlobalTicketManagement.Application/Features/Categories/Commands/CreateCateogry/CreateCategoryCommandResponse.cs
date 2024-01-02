using GlobalTicketManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTicketManagement.Application.Features.Categories.Commands.CreateCateogry
{
    public class CreateCategoryCommandResponse:BaseResponse
    {
        public CreateCategoryCommandResponse():base()
        {
                
        }

        public CreateCategoryDto Category { get; set; }
    }
}
