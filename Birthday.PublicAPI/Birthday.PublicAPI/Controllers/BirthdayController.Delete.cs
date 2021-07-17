using Birthday.Application.contracts;
using Microsoft.AspNetCore.Mvc;

using System.Threading;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers
{
    public partial class BirthdayController
    {
        // POST: BirthdayController/Delete/5
        [HttpDelete ("{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(
            [FromRoute]int id, CancellationToken cancellationToken)
        {
            await _birthdayService.Delete(new DeleteBirthday.Request {
                Id = id
            }, cancellationToken);
            
            return Ok();
        }
    }
}
