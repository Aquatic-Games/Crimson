#pragma once

#include <SDL3/SDL.h>

#include "CrimsonEngine/Platform/Surface.h"

namespace Crimson::Platform::SDL3
{
    class SDL3Surface final : public Surface
    {
    public:
        SDL_Window* Window;

        explicit SDL3Surface(const SurfaceInfo& info);
        ~SDL3Surface() override;
    };
}
