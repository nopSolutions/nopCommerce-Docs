// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See LICENSE file in the project root for full license information.
var common = require('./common.js');

/**
 * This method will be called at the start of exports.transform in conceptual.html.primary.js
 */
exports.preTransform = function (model) {
  return model;
}

/**
 * This method will be called at the end of exports.transform in conceptual.html.primary.js
 */
exports.postTransform = function (model) {
  if (model._nav && model.docurl) {

    var gitInfo = common.getGitInfo(model._gitContribute, model.source.remote);

    model.docurl = model.docurl.replace(gitInfo.path, gitInfo.path.replace(/^[A-Z]{2}\//i, 'en/'));
  }

  return model;
}