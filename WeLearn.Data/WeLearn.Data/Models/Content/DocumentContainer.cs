using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.EqualityCompareres;

namespace WeLearn.Data.Models.Content;

public class DocumentContainer : Content
{
    public DocumentContainer(
        long? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        string type,
        Guid? creatorId,
        Guid? externalSystemId,
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
            externalSystemId)
    {
        DocumentCount = 0;
    }

    public int DocumentCount { get; set; }

    public ICollection<Document> Documents { get; set; }

    public bool TryAddDocument(Document document)
    {
        if (Documents is null)
            Documents = new HashSet<Document>();

        if (Documents.Contains(document, new DocumentComparer()))
            return false;

        Documents.Add(document);
        DocumentCount++;
        return true;
    }

    public override void Update(Content content)
    {
        if (content is not DocumentContainer documentContainer)
            throw new ArgumentException("The given content is not a DocumentContainer");

        // TODO update documents
        // consider scenario when the imported content has fewer documents than existing one
        // according to the spec, we shouldn't allow for the losing of documents
        if (documentContainer.Documents is not null)
            foreach (var document in documentContainer.Documents)
            {
                TryAddDocument(document);
            }

        base.Update(content);
    }
}
