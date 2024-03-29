﻿namespace Organic.Contracts.Alert
{
    public static class AlertMessages
    {
        public const string ORDER_CREATED_TITLE_TO_MODERATOR = "New order created";
        public const string ORDER_CREATED_CONTENT_TO_MODERATOR = "{first_name} {last_name} created new order {tracking_code}";

        public const string ORDER_CREATED_TITLE_TO_OWNER = "New order created";
        public const string ORDER_CREATED_CONTENT_TO_OWNER = "You order successfully created {tracking_code}";
    }
}
