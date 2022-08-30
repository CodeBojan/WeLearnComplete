using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Comment;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.Comment;
using WeLearn.Auth.Controllers;
using WeLearn.Auth.Policy;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : UserAuthorizedController
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("content/{contentId}")]
    public async Task<ActionResult<PagedResponseDto<GetCommentDto>>> GetContentCommentsAsync([FromRoute] Guid contentId, [FromQuery] PageOptionsDto pageOptions)
    {
        try
        {
            var dtos = await _commentService.GetContentCommentsAsync(contentId, pageOptions);
            return Ok(dtos);
        }
        catch (ContentNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("content/{contentId}")]
    public async Task<ActionResult<GetCommentDto>> PostContentCommentAsync([FromBody] PostCommentDto postDto)
    {
        try
        {
            var dto = await _commentService.CreateCommentAsync(postDto.Body, UserId, postDto.ContentId);
            return Ok(dto);
            // TODO adjust exception handling
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("content/{contentId}")]
    public async Task<ActionResult<DeleteCommentDto>> DeleteContentCommentAsync([FromRoute] Guid contentId, [FromServices] IAuthorizationService authorizationService)
    {
        var result = await authorizationService.AuthorizeAsync(User, Policies.IsResourceAdmin);
        if (!result.Succeeded)
        {
            return Forbid();
        }

        try
        {
            var dto = await _commentService.DeleteCommentAsync(contentId);
            return Ok(dto);
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
    }
}
