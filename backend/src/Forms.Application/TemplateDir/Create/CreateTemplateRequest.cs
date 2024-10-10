using Forms.Application.DTOs;

namespace Forms.Application.TemplateDir.Create;

public record CreateTemplateRequest
{
    public TitleDto Title { get; set; } 
    public DescriptionDto Description { get; set; }
};