using Forms.Application.DTOs;

namespace Forms.Application.Template.Create;

public record CreateTemplateRequest
{
    public TitleDto Title { get; set; } 
    public DescriptionDto Description { get; set; }
};