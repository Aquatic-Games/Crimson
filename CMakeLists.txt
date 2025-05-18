cmake_minimum_required(VERSION 3.31)
project(CrimsonEngine)

set(CMAKE_CXX_STANDARD 20)

add_library(CrimsonEngine STATIC
        include/CrimsonEngine/Surface.cpp
        include/CrimsonEngine/Platform/Surface.h
        include/CrimsonEngine/Core/CoreUtils.h)

target_include_directories(CrimsonEngine PUBLIC include)

find_package(SDL3 REQUIRED)
target_link_libraries(CrimsonEngine PUBLIC SDL3::SDL3)
