/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_BREATH = 3698047240U;
        static const AkUniqueID PLAY_DOOR = 2547633870U;
        static const AkUniqueID PLAY_FOOTSTEPS_RUN = 2983625421U;
        static const AkUniqueID PLAY_FOOTSTEPS_WALK = 2618673757U;
        static const AkUniqueID PLAY_ROOM_TONES = 1501818189U;
        static const AkUniqueID PLAY_SCREAMS = 2700195184U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PLAYER
        {
            static const AkUniqueID GROUP = 1069431850U;

            namespace STATE
            {
                static const AkUniqueID IDLE = 1874288895U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SPOTTED = 3214304800U;
            } // namespace STATE
        } // namespace PLAYER

        namespace PLAYER_EYES
        {
            static const AkUniqueID GROUP = 4229160191U;

            namespace STATE
            {
                static const AkUniqueID CLOSED = 3012222945U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYER_EYES

    } // namespace STATES

    namespace SWITCHES
    {
        namespace DOOR
        {
            static const AkUniqueID GROUP = 1877847629U;

            namespace SWITCH
            {
                static const AkUniqueID CLOSE = 1451272583U;
                static const AkUniqueID OPEN = 3072142513U;
            } // namespace SWITCH
        } // namespace DOOR

        namespace FLOOR_TEXTURE
        {
            static const AkUniqueID GROUP = 1880793925U;

            namespace SWITCH
            {
                static const AkUniqueID CARPET = 2412606308U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace FLOOR_TEXTURE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID PLAYER_SPEED = 1062779386U;
        static const AkUniqueID STRESS_INTENSITY = 1289058627U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIANCE = 2981377429U;
        static const AkUniqueID INTERACT = 1466384055U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MONSTER = 2376328173U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID REVERBS = 3545700988U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID MEDIUM_VERB = 3055128616U;
        static const AkUniqueID SMALL_VERB = 3252130918U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
