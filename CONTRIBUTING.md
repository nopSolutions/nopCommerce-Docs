# Contribute to nopCommerce documentation

This document covers the process for contributing to the articles and code samples that are hosted on the [nopCommerce documentation site](http://docs.nopcommerce.com). Typo corrections and new articles are welcome contributions.

## How to make a simple correction or suggestion

Articles are stored in the repository as Markdown files. Simple changes to the content of a Markdown file are made in the browser by selecting the **Edit** link in the upper-right corner of the browser window. (In a narrow browser window, expand the **options** bar to see the **Edit** link.) Follow the directions to create a pull request (PR). We will review the PR and accept it or suggest changes.

## Docs Authoring Pack extension in Visual Studio Code

If you use Visual Studio Code to contribute to the nopCommerce documentation, you can boost your productivity by installing the Docs [Authoring Pack](https://marketplace.visualstudio.com/items?itemName=docsmsft.docs-authoring-pack) extension. The extension provides a variety of tools that helps with Markdown linting, code spell checking, and article templates.

## Markdown syntax

Articles are written in [DocFx-flavored Markdown](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html), which is a superset of [GitHub-flavored Markdown (GFM)](https://guides.github.com/features/mastering-markdown/).

## Folder structure conventions

Structuring rules:

- All resources related to the article (images, videos, etc.) should be stored in a separate folder. For example:

    ```sh
    _static/[topic_file_name]/[Resource_name].png
    ```

    where `[topic_file_name].md` is flush with the _static folder

- The folder that unites articles into a group should include the root file `index.md`

So, an image in the `en/developer/tutorials/source-code-organization.md` file is rendered by the following Markdown:

```md
![description of image for alt attribute](en/developer/tutorials/_static/source-code-organization/imagename.png)
```

All images should have [alternative (alt) text](https://wikipedia.org/wiki/Alt_attribute). For advice on specifying alt text, see online resources, such as [WebAIM: Alternative Text](https://webaim.org/techniques/alttext/).

Use lowercase for Markdown file names and image file names.

## Internal links

Internal links should use the uid of the target article with an xref link (link text is set to the linked content's title):

```md
<xref:uid_of_the_topic>
```

If the title of the article is unsuitable for link text (for example, a word or phrase in a sentence is the link text), specify the xref link and link text with the following:

```md
[link text](xref:uid_of_the_topic)
```

For more information, see the [DocFX Cross Reference](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html#cross-reference).

## Images and screenshots

Do not use images to publish source code.

This restriction reduce the size of the repository.

As an optional step, ensure that any images and screenshots used in the documentation are compressed, which helps with file size and page load performance. A few popular tools include TinyPNG (using the [TinyPNG website](https://tinypng.com/) or the [TinyPNG API](https://tinypng.com/developers)) or the [Image Optimizer](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ImageOptimizer) Visual Studio extension.

## Test changes with DocFX

Test your changes with the [DocFX command-line tool](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html#2-use-docfx-as-a-command-line-tool), which creates a locally hosted version of the site. DocFX doesn't render style and site extensions created for docs.microsoft.com.

## Forking and submitting pull requests

If you want to contribute some changes to nopCommerce documentation (article fix or some new post), then you should follow the next approach. Here is a short list of steps to contribute:

- First of all, you have to create a fork. Please find more about repository forking on GitHub at [https://help.github.com/articles/fork-a-repo/](https://help.github.com/articles/fork-a-repo/).
- Clone it locally.
- Create a new branch from "master". Please always create a new branch for each contribution. You should create it from our "master" branch only.
- Write the code and push back to your GitHub fork.
- Create pull request. Please read more about it at [https://help.github.com/articles/using-pull-requests/](https://help.github.com/articles/using-pull-requests/). And please always sync with our repository before doing it.

## Microsoft Writing Style Guide

The [Microsoft Writing Style Guide](https://docs.microsoft.com/style-guide/welcome/) provides writing style and terminology guidance for all forms of technology communication, including the nopCommerce documentation.

## Redirects

If you delete an article, change its file name, or move it to a different folder, create a redirect so that people who bookmarked the article don't receive a 404 Not Found error. Add redirects to the [web.config file](https://github.com/nopSolutions/nopCommerce-Docs/blob/master/web.config).
