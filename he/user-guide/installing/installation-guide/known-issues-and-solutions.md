---
title: Known Issues and Solutions
uid: he/user-guide/installing/installation-guide/known-issues-and-solutions
---

# Known Issues and Solutions

## Issue: Installation issues - 500 Internal Error

Server Error 500 - Internal server error. There is a problem with the resource you are looking for, and it cannot be displayed.

### Solution

- Select the server in IIS Manager.
- Select Feature Delegation.
- Set Modules to Read/Write

And also, the nopCommerce required to give access to the application pool’s identity.