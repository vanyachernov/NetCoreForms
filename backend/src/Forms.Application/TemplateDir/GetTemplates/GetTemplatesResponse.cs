using Forms.Application.DTOs;

namespace Forms.Application.TemplateDir.GetTemplates;

public record GetTemplatesResponse
{
    public Guid Id { get; set; }
    public TitleDto Title { get; set; }
    public DescriptionDto Description { get; set; }
};