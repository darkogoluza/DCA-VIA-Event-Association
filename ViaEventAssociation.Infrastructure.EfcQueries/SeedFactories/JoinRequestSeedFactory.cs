﻿using System.Text.Json;
using ViaEventAssociation.Infrastructure.EfcQueries.Models;

namespace ViaEventAssociation.Infrastructure.EfcQueries.SeedFactories;

public class JoinRequestSeedFactory
{
    private const string JoinRequestsAsJson = """
                                              [
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                  "Reason": "I\u0027m up for a challenge",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "This sounds interesting",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I want to try something new",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I seek community",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "I want to grow",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
                                                  "Reason": "I\u0027m excited to attend the workshops and gain valuable insights and knowledge.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                  "Reason": "I want to increase visibility",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                  "Reason": "I\u0027m intrigued by the topics being discussed and want to engage in meaningful conversations",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "27a5bde5-3900-4c45-9358-3d186ad6b2d7",
                                                  "GuestId": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
                                                  "Reason": "I\u0027m attending for the entertainment value, like the live music and performances.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                  "Reason": "I crave knowledge",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "This sounds interesting",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I value connections",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I see this event as an opportunity for personal growth and stepping out of my comfort zone.",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "I\u0027m seeking self-improvement",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                  "Reason": "I value connections",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I\u0027m intrigued by the topics being discussed and want to engage in meaningful conversations",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "bdf6156b-67a9-46d1-9b3e-8584f7f827c3",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I\u0027m keen on collaboration",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "I enjoy live shows",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I value connections",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I want to make new connections",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "I want to grow",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "31bf665d-7112-4c6a-80b4-6bfba7ce1f96",
                                                  "Reason": "I\u0027m looking forward to socializing with like-minded individuals and making new friends.",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I need some inspiration",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
                                                  "Reason": "I see this event as an opportunity to contribute my insights and expertise to discussions",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                  "Reason": "I\u0027m looking forward to socializing with like-minded individuals and making new friends.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
                                                  "Reason": "I\u0027m here to unwind",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "503457fb-dd03-40f6-8e89-79aee51a8736",
                                                  "Reason": "I want to discover new opportunities and potential partnerships",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "3bdd82ed-ce96-4bb7-89a5-5ab4e3239be7",
                                                  "Reason": "I want to have fun",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "bc997748-d3f9-47af-b9b5-d74dc89a8ae4",
                                                  "Reason": "I see this event as an opportunity to contribute my insights and expertise to discussions",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "74d098dc-069a-4a14-8841-65bec15c1e3e",
                                                  "Reason": "I\u0027m curious to meet people",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "c6ab5431-402c-4ffc-b43b-df2318ab5cc9",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "4f4a7b99-2e70-494e-ae96-7636391a860d",
                                                  "Reason": "I\u0027m excited to showcase my talents or expertise during the event",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "6f6e4b5a-0114-4be6-892c-a8fe015d702a",
                                                  "Reason": "I value connections",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "9bd01fdd-619c-4170-9573-100ccfea176b",
                                                  "GuestId": "46fc609c-ac4a-4186-9864-a635293f09a8",
                                                  "Reason": "I see this event as an opportunity for personal growth and stepping out of my comfort zone.",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "I value connections",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I see this event as an opportunity to contribute my insights and expertise to discussions",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I\u0027m eager to explore new ideas and perspectives presented at the event",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "I want to grow",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "31bf665d-7112-4c6a-80b4-6bfba7ce1f96",
                                                  "Reason": "I want to be active",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I need some inspiration",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "a2b432da-79ae-467b-9e1b-12a519b536c3",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I\u0027m attending to support a friend or colleague who is participating in the event",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                  "Reason": "I\u0027m eager to expand my professional circle and meet new contacts at the event.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "This sounds interesting",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I want to try something new",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I need some inspiration",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "31bf665d-7112-4c6a-80b4-6bfba7ce1f96",
                                                  "Reason": "I want to be social",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I\u0027m intrigued by the topics being discussed and want to engage in meaningful conversations",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "dada195b-5a2c-4856-9b0c-b1b8c23fac3a",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I\u0027m attending to support a friend or colleague who is participating in the event",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                  "Reason": "I want to have fun",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "I value connections",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I value connections",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I want to make new connections",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "Being part of this event\u0027s community offers me support and a sense of belonging.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                  "Reason": "I seek community",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I love live entertainment",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "e2f53fa6-1b36-4f3f-a61b-4d9840d6d1c3",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I\u0027m curious to meet people",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "f27723e5-da0b-4d23-94dd-a1805729bf63",
                                                  "Reason": "I want to grow",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I seek community",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "31bf665d-7112-4c6a-80b4-6bfba7ce1f96",
                                                  "Reason": "I want to be active",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I want to learn something new",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I see this event as an opportunity to contribute my insights and expertise to discussions",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
                                                  "Reason": "I need some inspiration",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                  "Reason": "I want to learn",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                  "Reason": "I see this event as an opportunity for personal growth and stepping out of my comfort zone.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
                                                  "Reason": "I\u0027m attending for the entertainment value, like the live music and performances.",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c1a4c47e-b34f-46ad-b122-15cf5ffd1196",
                                                  "GuestId": "503457fb-dd03-40f6-8e89-79aee51a8736",
                                                  "Reason": "I want to discover new opportunities and potential partnerships",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "I value connections",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I seek community",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "I need motivation",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I\u0027m attending to support a friend or colleague who is participating in the event",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "6839738e-94c6-49fa-8bb4-2c02afd34eae",
                                                  "Reason": "I see this event as an opportunity to contribute my insights and expertise to discussions",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                  "Reason": "I\u0027m looking forward to some relaxation and leisure activities at the event.",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                  "Reason": "I love live entertainment",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "c94d3832-a2c8-493f-a6fb-174b991a6101",
                                                  "GuestId": "5a4ba5f3-c11a-47e7-80b7-564818ca106d",
                                                  "Reason": "I\u0027m up for a challenge",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "230c1a99-d5c7-4fbc-9f48-07ccbb100936",
                                                  "Reason": "I want to meet new people",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "2e31b742-5826-49da-8a1f-784ea0473e02",
                                                  "Reason": "I want to have fun",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "46f255d5-3769-4c48-87a8-c288c0bbc468",
                                                  "Reason": "I enjoy live shows",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "2e09e219-f919-46a8-91f7-e5a48874480b",
                                                  "Reason": "I need some inspiration",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "954931e0-88ca-4598-ab70-290be22e16b5",
                                                  "Reason": "I see this event as an opportunity to contribute my insights and expertise to discussions",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "0310a038-576e-4b98-b0e6-b67647db1be3",
                                                  "Reason": "I\u0027m looking forward to some relaxation and leisure activities at the event.",
                                                  "Status": "Accepted"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "2dff5453-f686-4faf-8323-74ad56c97b1a",
                                                  "Reason": "I\u0027m intrigued by the topics being discussed and want to engage in meaningful conversations",
                                                  "Status": "Declined"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "1b92c7b9-fb0d-4b57-8e99-c58912be305f",
                                                  "Reason": "I\u0027m attending to support a friend or colleague who is participating in the event",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "c784502a-16a9-41c5-a56d-2d6b24234ae0",
                                                  "Reason": "I need motivation",
                                                  "Status": "Pending"
                                                },
                                                {
                                                  "EventId": "d95faaf1-4261-4df6-b942-68efb0a5f0ee",
                                                  "GuestId": "bee560a4-6145-47b4-ba6d-fc530ce89d96",
                                                  "Reason": "I love live entertainment",
                                                  "Status": "Declined"
                                                }
                                              ]
                                              """;

    private static void AddJoinRequestToEvent(VeadatabaseProductionContext context, string eventId)
    {
        List<TempJoinRequests> tempJoinRequestsList =
            JsonSerializer.Deserialize<List<TempJoinRequests>>(JoinRequestsAsJson)!;

        IEnumerable<RequestToJoin> requestToJoins = tempJoinRequestsList
            .Where(rtj => rtj.EventId == eventId)
            .Select(rtj => new RequestToJoin
            {
                Id = Guid.NewGuid().ToString(),
                VeaEventId = rtj.EventId,
                InvitorId = rtj.GuestId,
                StatusType = rtj.Status,
                Reason = rtj.Reason
            });

        context.RequestToJoins.AddRange(requestToJoins);
    }

    public static void Seed(VeadatabaseProductionContext context)
    {
        foreach (var veaEvent in context.Events)
        {
            AddJoinRequestToEvent(context, veaEvent.Id);
        }
    }
}

public record TempJoinRequests(string EventId, string GuestId, string Reason, string Status);
