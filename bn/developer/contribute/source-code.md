---
title: Working with source code and contributions
uid: en/developer/contribute/source-code
author: git.AndreiMaz
contributors: git.RomanovM, git.exileDev
---

# Working with source code and contributions

## Checking out source code

nopCommerce manages a repository at GitHub ([https://github.com/nopSolutions/nopCommerce](https://github.com/nopSolutions/nopCommerce)). So you can always check out the latest source code! Git SCM (Source Code Management) access is public and allows you to fetch in real time the latest version of nopCommerce! It allows you to follow daily nopCommerce developments and improvements. Get the latest patches, fixes without waiting for the next release. If you're not familiar with Git, there's a good and free documentation [here](https://git-scm.com/docs). Also find more info about GitHub support [here](https://opensource.guide/how-to-contribute/). Please note that these versions should not be used in a production environment. We do not guarantee that any of the functionality or code found in our SCM (Source Code Management) will be made available in our official releases. The best way to get the source code is to clone the repository. Git comes with built-in GUI tools for committing (git-gui) and browsing (gitk), but there are several third-party tools for users looking for platform-specific experience. Please find them at [https://git-scm.com/downloads/guis](https://git-scm.com/downloads/guis) (we use [SourceTree](https://www.sourcetreeapp.com/)).

## Branch Descriptions and Naming

Recently we started using Vincent Driessen's branching model (seen here: [http://nvie.com/posts/a-successful-git-branching-model/](https://nvie.com/posts/a-successful-git-branching-model/)) including the use of feature branches, a development branch (for integration) and a master branch (for publishing/production). Previously we had "master" branch only (till January 2016)

* Production branch: master
* Development branch: develop
* Feature and issue branches: Should start with "feature" or "issue". It should be followed by issue ID (according to our Github issue list) and some friendly name (example, "multistore"). Finally, it should look like "feature-34-multistore" or "issue-35-paypal-redirection-bug"
* Release branches: Should start with "release". It should be followed by version number (example, "3.00"). Finally, it should look like "release-3.00"

## Forking and submitting pull requests

If you want to contribute some source code to nopCommerce core (issue fix or some new feature), then you should follow the next approach. Here is a short list of steps to contribute:

* First of all, you have to create a fork. Please find more about repository forking on GitHub at [https://help.github.com/articles/fork-a-repo/](https://help.github.com/articles/fork-a-repo/).
* Clone it locally.
* Create a new branch from "develop". Please always create a new branch for each contribution. You should create it from our "develop" branch only. Do not use "master".
* Write the code and push back to your GitHub fork.
* Create pull request. Please read more about it at [https://help.github.com/articles/using-pull-requests/](https://help.github.com/articles/using-pull-requests/). And please always sync with our repository before doing it.
