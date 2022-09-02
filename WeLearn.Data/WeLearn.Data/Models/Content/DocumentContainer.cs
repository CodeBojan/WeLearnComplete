using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.EqualityCompareres;

namespace WeLearn.Data.Models.Content;

public class DocumentContainer : Content
{
    internal DocumentContainer()
    {
    }

    public DocumentContainer(
        string? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        string type,
        Guid? creatorId,
        Guid? externalSystemId,
        DateTime? externalCreatedDate,
        int documentCount = 0) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            courseId,
            type,
            creatorId,
            externalSystemId,
            externalCreatedDate)
    {
        DocumentCount = 0;
    }

    public int DocumentCount { get; set; }

    public ICollection<Document> Documents { get; set; }

    public bool TryAddDocument(Document document)
    {
        if (Documents is null)
            Documents = new HashSet<Document>();

        var equalityComparer = new DocumentComparer();
        var existingDocument = Documents.FirstOrDefault(d => equalityComparer.Equals(d, document));
        if (existingDocument is not null)
        {
            if (existingDocument.ExternalUrl != document.ExternalUrl)
            {
                existingDocument.ExternalUrl = document.ExternalUrl;
                return true;
            }

            return false;
        }

        Documents.Add(document);
        DocumentCount++;
        return true;
    }

    public override void Update(Content content)
    {
        if (content is not DocumentContainer documentContainer)
            throw new ArgumentException("The given content is not a DocumentContainer");

        // Note: Consider scenario when the imported content has fewer documents than existing one
        // according to the spec, we shouldn't allow for the losing of documents
        if (documentContainer.Documents is not null)
            foreach (var document in documentContainer.Documents)
            {
                TryAddDocument(document);
            }

        base.Update(content);
    }
}
