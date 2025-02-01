# Forum - Assignment

The forum project can be themed around any kind of topics. It should provide its users with the functionality to register and login, post threads, comment on threads, reply to comments, add attachments, react to comments and threads etc. 

# Overview

Forums are usually pretty plain so the main thing people focus on keeping their interest in a forum is their engagement and their status.

# Main entities

* Users
* Custom roles
* Threads (posts)
* Comments (and replies)
* Reactions
* Attachments
* Categories

# Notable features

## User Registration

Users should be capable of registering and upon successful registration, a success message should be presented to the user and after a short timeout they should be successfully logged in the forum.

## Email verification 

Upon registration, users should receive a verification email on their appointed email. They should be allowed to enter the web store without being verified, however, they should not be capable of completing their cart until they have verified their email.

## Attachments

The forum should support file-based attachments that are stored in a cloud-like storage.

If the attachments are text-based, they should be visualized in some kind of embedded text viewer.

If the attachments are picture-based, they should be visualized in some kind of image viewer.
In any other case, the file should be visualized in some kind of generic file viewer.

## Forum Search

Users should be capable of applying an advanced search through users, threads, comments, replies, attachments etc.

## Threads

Users should be capable of posting a thread in a particular category. The thread should have a title and a basic description with or without attachments.

## Comments & Replies

Users can comment on Threads. Comments can also hold file attachments.

Replies can be made to a comment. Replies are essentially comments, but they hold a reference to the original comment that is being replied to. This introduces a recursive tree-like structure of replies.

## Reactions

Users should be capable of reacting to threads, comments, replies etc. Reactions can vary: you should provide the users with many reactions. There should be a reaction count on the threads / comments / replies.

## Tags

Users should be capable of tagging other users in their threads, comments, replies.

## Notifications

Users should be notified about comments and reactions on their threads, tags in comments and threads and replies, bans, blocks, deleted videos, hidden posts by administration, etc.

## User Profiles

Users should have their activity tracked by the forum and exposed on their public profile (if they desire so - this should be configurable). 

Users should be capable of configuring what statistics to show in their public profile, e.g. threads created, comments posted (replies count here), reactions to posts. This can be aggregated into some kind of forum points system.

There should be some kind of leaderboard ranking of forum users based on activity.

There should be roles based on the activity. You should gain a specific role for milestones in activity. (e.g. 1000 threads posted -> gives you the role “Spammer”)

## Administration

The forum administration is an essential panel of the project.

You can pretty much CRUD any entity in the administration. Introduce an intuitive design to support this functionality. 

You can modify user roles through the administration.

You can define roles and role conditions. Role conditions should be checked each time a user activity is performed or an award is given and if the condition evaluates to truthy, the user should be given the role.

Awards can be defined through the administration and award conditions. Awards are given to a player when he fulfills the condition through an activity or through administrator favor.

Administrators and Moderators can ban users, although Moderators should only be capable of timing out (banning a user) for 24 hours maximum.

Administrators and Moderators should be capable of hiding a thread / comment / reply / attachment if they deem it not acceptable according to the forum community standards and rules.

Administrators should be capable of defining categories in the forum.

NOTE: Moderators do NOT have (or have very limited) access to the Administration.

## Authorities

A User is capable of full CRUD over his own threads / comments / replies / attachments. 

There should also be a Moderator role, which has the authority of CRUD over any thread / comment / reply / attachment.

There should be an Administrator authority which has access to the Administration and full sovereignty over the forum.

## Custom user roles

* Label
* CSS Color / Gradient
* Authority (identity role)

## Additional

Reddit cateogories are actually users (something like page users in Facebook or GitHub organization users)

Our Categories need some level of moderation.
* The current idea: Category creation request.
* Public pool: Here a person can propose up to 1 category creation request.
* Private pool: (Verified Users - there is monetization here (payments with Stripe)). Paid users can propose up to 3 category creation requests, 
  and they don't need votes. A moderator should see the category creation request and if it's okay, create it without votes.
* Community privacy


