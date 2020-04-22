---
title: Payment statuses
uid: en/user-guide/configuring/setting-up/payments/payment-statuses
author: git.AndreiMaz
contributors: git.exileDev, git.ivkadp
---

# Payment statuses

You can check **payment statuses in Sales → Orders :**

* **Pending:** when a transaction is not approved yet.

* **Authorized:** when the amount was charged, but not captured or transferred, just the credit card was verified.

> [!TIP]
> 
> If you do not want to charge the customer until you ship, then use Authorize. For charges that come in as Authorized only, you can later capture them via the Administration area using the Capture button on the order page.

* **Partially refunded:** when order paid and partially refunded.

* **Voided:** when an order was cancelled before get paid.

> [!WARNING]
> 
> This mode is available only when its payment status is Authorized.

* **Refunded:** when an order was cancelled or returned and the amount needs to be refunded after get paid.

> [!WARNING]
> 
> This mode is available only when its payment status is Paid (meaning, captured).

* **Partially refunded:** when an order partially was cancelled and the amount needs to be refunded after get paid.

> [!WARNING]
> 
> This mode is available only when its payment status is Paid (meaning, captured).

* **Paid:** when the order was Paid.
