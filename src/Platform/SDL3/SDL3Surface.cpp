#include "SDL3Surface.h"

namespace Crimson::Platform::SDL3
{
    SDL3Surface::SDL3Surface(const SurfaceInfo& info)
    {
        if (!SDL_Init(SDL_INIT_VIDEO))
            throw std::runtime_error(std::format("Failed to initialize SDL: {}", SDL_GetError()));

        Window = SDL_CreateWindow(info.Title.c_str(), info.Size.Width, info.Size.Height, SDL_WINDOW_VULKAN);

        if (!Window)
            throw std::runtime_error(std::format("Failed to create SDL window: {}", SDL_GetError()));
    }

    SDL3Surface::~SDL3Surface()
    {
        SDL_DestroyWindow(Window);
        SDL_Quit();
    }
}
