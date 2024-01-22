#!/bin/bash
BUILD_PATH=$1
echo "Build Path: $BUILD_PATH"

FASTLANE_DIR="$BUILD_PATH/fastlane"

if [ ! -d "$FASTLANE_DIR" ]; then
    mkdir -p "$FASTLANE_DIR"
    touch "$FASTLANE_DIR/Fastfile"

    echo "default_platform(:ios)" >> "$FASTLANE_DIR/Fastfile"
    echo "" >> "$FASTLANE_DIR/Fastfile"
    echo "platform :ios do" >> "$FASTLANE_DIR/Fastfile"
    echo "  desc 'Deploy the app to TestFlight/App Store'" >> "$FASTLANE_DIR/Fastfile"
    echo "  lane :deploy do" >> "$FASTLANE_DIR/Fastfile"
    echo "    gym(export_method: 'app-store', project: 'Unity-iPhone.xcodeproj', scheme: 'Unity-iPhone', output_directory: 'builds/')" >> "$FASTLANE_DIR/Fastfile"
    echo "    upload_to_app_store(skip_metadata: true, skip_screenshots: true, skip_binary_upload: false)" >> "$FASTLANE_DIR/Fastfile"
    echo "  end" >> "$FASTLANE_DIR/Fastfile"
    echo "end" >> "$FASTLANE_DIR/Fastfile"
fi
