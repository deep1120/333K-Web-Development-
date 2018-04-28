
//using System.Data.Entity.Infrastructure;
//using System.Web.Mvc;


//DbModel IEnumerable<MIS333K_Team11_FinalProjectV2.Models.AppUser>

//@{
//    ViewPage.Title = "ManagerIndex";
//}

//<h2>User Database</h2>

//<table class="table">
//    <tr>
//        <th>
//            @Html.DisplayNameFor(model => model.FirstName)
//        </th>
//        <th>
//            @Html.DisplayNameFor(Model => model.MiddleInitial)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.LastName)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.StreetAddress)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.ZipCode)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.Email)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.EmailConfirmed)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.PasswordHash)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.SecurityStamp)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.PhoneNumber)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.PhoneNumberConfirmed)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.TwoFactorEnabled)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.LockoutEndDateUtc)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.LockoutEnabled)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.AccessFailedCount)
//        </th>
//        <th>
//            @Html.DisplayNameFor(model => model.UserName)
//        </th>
//        <th></th>
//    </tr>

//@foreach(var item in Model)
//{
//    < tr >
//        < td >
//            @Html.DisplayFor(modelItem => item.FirstName)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.MiddleInitial)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.LastName)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.StreetAddress)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.ZipCode)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.Email)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.EmailConfirmed)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.PasswordHash)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.SecurityStamp)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.PhoneNumber)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.PhoneNumberConfirmed)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.TwoFactorEnabled)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.LockoutEndDateUtc)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.LockoutEnabled)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.AccessFailedCount)
//        </ td >
//        < td >
//            @Html.DisplayFor(modelItem => item.UserName)
//        </ td >
//        < td >
//            @Html.ActionLink("Edit", "Edit", new { id = item.Id }) |
//            @Html.ActionLink("Details", "Details", new { id = item.Id }) |
//            @Html.ActionLink("Delete", "Delete", new { id = item.Id })
//        </ td >
//    </ tr >
//}

//</Table>